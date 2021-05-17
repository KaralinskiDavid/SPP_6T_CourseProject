import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap'
import { Student } from '../classes/iismodels';

@Component({
  selector: 'app-confirm-delete-modal',
  templateUrl: './confirm-delete-modal.component.html',
  styleUrls: ['./confirm-delete-modal.component.css']
})
export class ConfirmDeleteModalComponent implements OnInit {

  constructor(public modal: NgbActiveModal) {

  }

  user: Student;
  ngOnInit() {

  }

}
