import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap'
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm, ValidatorFn, AbstractControl } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-add-section-modal',
  templateUrl: './add-section-modal.component.html',
  styleUrls: ['./add-section-modal.component.css']
})
export class AddSectionModalComponent implements OnInit {

  nameFormControl = new FormControl('', [
    Validators.required,
  ]);

  name: string;

  constructor(public modal: NgbActiveModal) { }

  ngOnInit() {
  }

}
