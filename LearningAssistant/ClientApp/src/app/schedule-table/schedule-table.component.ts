import { MatTableDataSource } from '@angular/material/table'
import { MatPaginator } from '@angular/material/paginator';
import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { Schedule, DaySchedule, Lesson, LessonType } from '../classes/iismodels';
import { ScheduleService } from '../services/schedule.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-schedule-table',
  templateUrl: './schedule-table.component.html',
  styleUrls: ['./schedule-table.component.css']
})
export class ScheduleTableComponent implements OnInit {


  groupSchedule: Schedule;
  currentWeek: number;
  currentDay: number;
  selectedWeek: number;
  selectedDay: number;
  selectedDate: Date;
  currentDate: Date;
  displayedDate: string;

  displayedColumns: string[] = ['subject', 'subGroup', 'time', 'auditory', 'lessonType', 'queues'];
  dataSource: MatTableDataSource<Lesson>;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private _scheduleService: ScheduleService, private toastr: ToastrService) { }

  color = 'accent';
  checked = false;
  disabled = false;

  ngOnInit(): void {
    this.currentDate = new Date(Date.now());
    this.currentDay = this.currentDate.getDay();
    this.selectedDate = this.currentDate;
    this.selectedDay = this.currentDay;

    this._scheduleService.getCurrentWeek().subscribe((result: number) => {
      this.currentWeek = result;
      this._scheduleService.getGroupSchedule("861401").subscribe((result: Schedule) => {
        this.groupSchedule = result;
        let currentWeek = this.currentWeek.toString();
        this.dataSource = new MatTableDataSource<Lesson>(result.daySchedules[this.currentDay].lessons.filter(function (value: Lesson) {
          return value.weekNumber.includes('0') || value.weekNumber.includes(currentWeek);
        }));
      });
    });
  }

  selectedDateChanged() {
    this.selectedDay = this.selectedDate.getDay();
    let differ = this.diff_weeks(this.selectedDate, this.currentDate) % 4;
    if (this.currentDate < this.selectedDate)
      this.selectedWeek = this.currentWeek + differ > 4 ? 4 - this.currentWeek + differ : this.currentWeek + differ;
    else if (this.currentDate > this.selectedDate)
      this.selectedWeek = this.currentWeek - differ < 1 ? 4 + this.currentWeek - differ : this.currentWeek - differ;
    let selectedWeek = this.selectedWeek.toString();
    if (this.groupSchedule.daySchedules[this.selectedDay - 1]) {
      this.dataSource = new MatTableDataSource<Lesson>(this.groupSchedule.daySchedules[this.selectedDay - 1].lessons.filter(function (value: Lesson) {
        return value.weekNumber.includes('0') || value.weekNumber.includes(selectedWeek);
      }));
    }
    else
      this.dataSource = new MatTableDataSource<Lesson>(null);
  }

  diff_weeks(dt2, dt1) {
    var diff = (dt2.getTime() - dt1.getTime()) / 1000;
    diff /= (60 * 60 * 24 * 7);
    return Math.abs(Math.round(diff));
  }

}
