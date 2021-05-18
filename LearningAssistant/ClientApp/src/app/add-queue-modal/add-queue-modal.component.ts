import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Student } from '../classes/iismodels';

@Component({
  selector: 'app-add-queue-modal',
  templateUrl: './add-queue-modal.component.html',
  styleUrls: ['./add-queue-modal.component.css']
})
export class AddQueueModalComponent implements OnInit {

  students: Student[] = new Array<Student>();

  constructor(public modal: NgbActiveModal) { }

  ngOnInit() {
  }

}
