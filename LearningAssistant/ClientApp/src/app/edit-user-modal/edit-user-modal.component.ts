import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap'
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm, ValidatorFn, AbstractControl } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { User } from '../classes/user';
import { GroupService } from '../services/group.service';


export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

export interface Role {
  text: string;
  value: string;
}

@Component({
  selector: 'app-edit-user-modal',
  templateUrl: './edit-user-modal.component.html',
  styleUrls: ['./edit-user-modal.component.css']
})
export class EditUserModalComponent implements OnInit {

  constructor(public modal: NgbActiveModal, private _authService: AuthService,
    private _groupService: GroupService, public toastr: ToastrService) { }
  createdUser: User;

  roles: Role[] = [
    { text: 'Group headman', value: 'GroupHeadman' },
    { text: 'Speciality headman', value: 'SpecialityHeadman' },
    { text: 'Student', value: 'Student' }
  ];

  duplicateName = false;

  firstNameFormControl = new FormControl('', [
    Validators.required,
  ]);

  lastNameFormControl = new FormControl('', [
    Validators.required,
  ]);

  middleNameFormControl = new FormControl('', [
    Validators.required,
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(6),
  ]);

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  roleNameFormControl = new FormControl('', [
    Validators.required,
  ]);

  groupNumberFormControl = new FormControl('', [
    Validators.required,
  ]);

  subGroupNumberFormControl = new FormControl('', [
  ]);

  matcher = new MyErrorStateMatcher();

  checkEmail() {
    if (this.createdUser.email != null && this.createdUser.email.length > 0) {
      this._authService.checkEmail(this.createdUser.email).subscribe((result: boolean) => {
        this.duplicateName = result;
        if (result)
          this.emailFormControl.setErrors({ incorrect: true });
        else
          this.emailFormControl.setErrors(null);
      });
    }
  }

  checkGroupNumber() {
    if (this.createdUser.groupNumber != null && this.createdUser.groupNumber.length > 0)
      this._groupService.checkGroupNumber(this.createdUser.groupNumber).subscribe((result: boolean) => {
        if (!result)
          this.groupNumberFormControl.setErrors({ incorrect: true });
        else
          this.groupNumberFormControl.setErrors(null);
      });
  }

  ngOnInit() {
    this.createdUser = new User();
  }

}
