import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Student, Lesson, Queue } from '../classes/iismodels';
import { StudentService } from '../services/student.service';
import { ToastrService } from 'ngx-toastr';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { QueueService } from '../services/queue.service';

@Component({
  selector: 'app-queue-editor',
  templateUrl: './queue-editor.component.html',
  styleUrls: ['./queue-editor.component.css']
})
export class QueueEditorComponent implements OnInit {

  @Input() lesson: Lesson;
  @Input() groupNumber: string;
  @Input() selectedDate: string;
  @Input() subGroup: number;


  @Output() showTable = new EventEmitter<boolean>();

  students: Student[];
  queue: Student[] = new Array<Student>();

  constructor(private _studentService: StudentService, private _queueService: QueueService, private toastr: ToastrService) { }

  ngOnInit() {
    this.loadStudents();
  }

  shuffle(array) {
    return array.sort(() => Math.random() - 0.5);
  }

  randomGenerate() {
    if (this.students.length>0 && this.queue.length==0) {
      Object.assign(this.queue,this.shuffle(this.students));
      this.students.splice(0, this.students.length);
    }
    else {
      this.shuffle(this.queue);
    }
  }

  loadStudents() {
    this._studentService.getStudentsByGroupNumber(this.groupNumber).subscribe((result: Student[]) => {
      this.students = result;
    },
      error => {
        this.toastr.error("Something went wrong");
      }
    );
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }

  onSaveClicked() {
    let studentIds ="";
    this.queue.forEach((value: Student) => {
      studentIds += value.id.toString() + " ";
    });
    let queue = new Queue();
    queue.date = this.selectedDate;
    queue.lessonId = this.lesson.id;
    queue.studentIds = studentIds;
    queue.subGroup = this.subGroup.toString();
    this._queueService.createQueue(queue).subscribe((result: boolean) => {
      this.toastr.success("Created");
      this.showTable.emit();
      this.loadStudents();
      this.queue.splice(0, this.queue.length);
    },
      error => {
        this.loadStudents();
        this.queue.splice(0, this.queue.length);
        this.toastr.error("Something went wrong");
      }
    );
  }

  onCancelClicked() {
    this.loadStudents();
    this.queue.splice(0, this.queue.length);
    this.showTable.emit();
  }

}
