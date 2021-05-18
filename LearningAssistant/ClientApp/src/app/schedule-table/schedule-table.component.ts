import { MatTableDataSource } from '@angular/material/table'
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { Schedule, DaySchedule, Lesson, LessonType, Student } from '../classes/iismodels';
import { ScheduleService } from '../services/schedule.service';
import { ToastrService } from 'ngx-toastr';
import { AddQueueModalComponent } from '../add-queue-modal/add-queue-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { StudentService } from '../services/student.service';
import { QueueService } from '../services/queue.service';

@Component({
  selector: 'app-schedule-table',
  templateUrl: './schedule-table.component.html',
  styleUrls: ['./schedule-table.component.css']
})
export class ScheduleTableComponent implements OnInit {

  selectedLesson: Lesson;
  queueEditorDisabled = true;
  scheduleTableDisabled = false;

  students: Student[];

  groupSchedule: Schedule;
  currentWeek: number;
  currentDay: number;
  selectedWeek: number;
  selectedDay: number;
  selectedDate: Date;
  currentDate: Date;
  displayedDate: string;
  groupNumber: string;
  subGroup: number = null;
  userRole: string;

  displayedColumns: string[] = ['subject', 'subGroup', 'time', 'auditory', 'lessonType', 'queues'];
  dataSource: MatTableDataSource<Lesson>;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private _queueService: QueueService, private _scheduleService: ScheduleService, private _studentService: StudentService, private toastr: ToastrService, private _modalService: NgbModal) { }

  color = 'accent';
  checked = false;
  disabled = this.subGroup==null;

  ngOnInit(): void {
    this.groupNumber = localStorage.getItem("groupNumber");
    this.userRole = localStorage.getItem("role");
    this.subGroup = localStorage.getItem("subGroup") ? parseInt(localStorage.getItem("subGroup")) : null;
    this.currentDate = new Date(Date.now());
    this.currentDay = this.currentDate.getDay();
    this.selectedDate = this.currentDate;
    this.selectedDay = this.currentDay;

    this._studentService.getStudentsByGroupNumber(this.groupNumber).subscribe((result: Student[]) => {
      this.students = result;
    },
      error => { }
    )

    this._scheduleService.getCurrentWeek().subscribe((result: number) => {
      this.currentWeek = result;
      this.selectedWeek = this.currentWeek;
      this._scheduleService.getGroupSchedule(this.groupNumber).subscribe((result: Schedule) => {
        this.groupSchedule = result;
        let currentWeek = this.currentWeek.toString();
        if (this.groupSchedule.daySchedules[this.currentDay - 1]) {
          this.dataSource = new MatTableDataSource<Lesson>(result.daySchedules[this.currentDay-1].lessons.filter(function (value: Lesson) {
            return value.weekNumber.includes('0') || value.weekNumber.includes(currentWeek);
          }));
        }
        else {
          this.dataSource = new MatTableDataSource<Lesson>(null);
        }
      });
    });
  }

  isQueueForSelectedDate(element: Lesson) {
    if (element.queues.find((value => {
      return this.selectedDate.toLocaleDateString() == new Date(value.date).toLocaleDateString();
    })))
      return true;
    return false;
  }

  selectedDateChanged() {
    this.selectedDay = this.selectedDate.getDay();
    let differ = this.diff_weeks(this.selectedDate, this.currentDate) % 4;
    if (this.currentDate < this.selectedDate)
      this.selectedWeek = this.currentWeek + differ > 4 ? 4 - this.currentWeek + differ : this.currentWeek + differ;
    else if (this.currentDate > this.selectedDate)
      this.selectedWeek = this.currentWeek - differ < 1 ? 4 + this.currentWeek - differ : this.currentWeek - differ;
    let selectedWeek = this.selectedWeek.toString();
    let subGroup = this.subGroup.toString();
    let checked = this.checked;
    if (this.groupSchedule.daySchedules[this.selectedDay - 1]) {
      this.dataSource = new MatTableDataSource<Lesson>(this.groupSchedule.daySchedules[this.selectedDay - 1].lessons.filter(function (value: Lesson) {
        return (value.subGroup == subGroup || value.subGroup == "0" || !checked) && (value.weekNumber.includes('0') || value.weekNumber.includes(selectedWeek));
      }));
    }
    else
      this.dataSource = new MatTableDataSource<Lesson>(null);
  }

  refreshSchedule() {
    this._scheduleService.refreshGroupSchedule(this.groupNumber).subscribe((result: boolean) => {
      this.toastr.success("Refreshed");
      this._scheduleService.getGroupSchedule(this.groupNumber).subscribe((result: Schedule) => {
        this.groupSchedule = result;
        let selectedWeek = this.selectedWeek.toString();
        let subGroup = this.subGroup.toString();
        let checked = this.checked;
        if (this.groupSchedule.daySchedules[this.selectedDay - 1]) {
          this.dataSource = new MatTableDataSource<Lesson>(this.groupSchedule.daySchedules[this.selectedDay - 1].lessons.filter(function (value: Lesson) {
            return (value.subGroup == subGroup || value.subGroup == "0" || !checked) && (value.weekNumber.includes('0') || value.weekNumber.includes(selectedWeek));
          }));
        }
        else
          this.dataSource = new MatTableDataSource<Lesson>(null);
      });
    },
      error => {
        this.toastr.error("Something went wrong");
      }
    )
  }

  showQueue(element: Lesson) {
    var queue = element.queues.find((value => {
      return this.selectedDate.toLocaleDateString() == new Date(value.date).toLocaleDateString();
    }));
    var studentIds = queue.studentIds.split(" ");
    var students = new Array<Student>();
    studentIds.forEach((value) => {
      let studentId = value;
      let student = this.students.find((value) => {
        return value.id.toString() == studentId;
      });
      if(student)
        students.push(student);
    });
    const modalRef = this._modalService.open(AddQueueModalComponent);
    modalRef.componentInstance.students = students;
    modalRef.result.then((result) => {
      
    }).catch((res) => { });
  }

  deleteQueue(element: Lesson) {
    var queue = element.queues.find((value => {
      return this.selectedDate.toLocaleDateString() == new Date(value.date).toLocaleDateString();
    }));
    this._queueService.deleteQueue(queue.id).subscribe((result: boolean) => {
      if (result) {
        this.toastr.success("Deleted");
        this._scheduleService.getGroupSchedule(this.groupNumber).subscribe((result: Schedule) => {
          this.groupSchedule = result;
          let selectedWeek = this.selectedWeek.toString();
          let subGroup = this.subGroup.toString();
          let checked = this.checked;
          if (this.groupSchedule.daySchedules[this.selectedDay - 1]) {
            this.dataSource = new MatTableDataSource<Lesson>(this.groupSchedule.daySchedules[this.selectedDay - 1].lessons.filter(function (value: Lesson) {
              return (value.subGroup == subGroup || value.subGroup == "0" || !checked) && (value.weekNumber.includes('0') || value.weekNumber.includes(selectedWeek));
            }));
          }
          else
            this.dataSource = new MatTableDataSource<Lesson>(null);
        });
      }
      else
        this.toastr.error("Something went wrong");
    },
      error => { this.toastr.error("Something went wrong");}
    );
  }

  createQueue(lesson) {
    this.selectedLesson = lesson;
    this.queueEditorDisabled = false;
    this.scheduleTableDisabled = true;
  }

  diff_weeks(dt2, dt1) {
    var diff = (dt2.getTime() - dt1.getTime()) / 1000;
    diff /= (60 * 60 * 24 * 7);
    return Math.abs(Math.round(diff));
  }

  showTable() {
    this.scheduleTableDisabled = false;
    this.queueEditorDisabled = true;
    let selectedWeek = this.selectedWeek.toString();
    let subGroup = this.subGroup.toString();
    let checked = this.checked;
    this._scheduleService.getGroupSchedule(this.groupNumber).subscribe((result: Schedule) => {
      this.groupSchedule = result;
      let selectedWeek = this.selectedWeek.toString();
      let subGroup = this.subGroup.toString();
      let checked = this.checked;
      if (this.groupSchedule.daySchedules[this.selectedDay - 1]) {
        this.dataSource = new MatTableDataSource<Lesson>(this.groupSchedule.daySchedules[this.selectedDay - 1].lessons.filter(function (value: Lesson) {
          return (value.subGroup == subGroup || value.subGroup == "0" || !checked) && (value.weekNumber.includes('0') || value.weekNumber.includes(selectedWeek));
        }));
      }
      else
        this.dataSource = new MatTableDataSource<Lesson>(null);
    });
  }

  filterSubGroup(event) {
    if (!this.checked) {
      let selectedWeek = this.selectedWeek.toString();
      let subGroup = this.subGroup.toString();
      if (this.groupSchedule.daySchedules[this.selectedDay - 1]) {
        this.dataSource = new MatTableDataSource<Lesson>(this.groupSchedule.daySchedules[this.selectedDay - 1].lessons.filter(function (value: Lesson) {
          return (value.subGroup == subGroup || value.subGroup=="0") && (value.weekNumber.includes('0') || value.weekNumber.includes(selectedWeek));
        }));
      }
      else
        this.dataSource = new MatTableDataSource<Lesson>(null);
    }
    else {
      let selectedWeek = this.selectedWeek.toString();
      let subGroup = this.subGroup.toString();
      if (this.groupSchedule.daySchedules[this.selectedDay - 1]) {
        this.dataSource = new MatTableDataSource<Lesson>(this.groupSchedule.daySchedules[this.selectedDay - 1].lessons.filter(function (value: Lesson) {
          return (value.weekNumber.includes('0') || value.weekNumber.includes(selectedWeek));
        }));
      }
      else
        this.dataSource = new MatTableDataSource<Lesson>(null);
    }
  }

}
