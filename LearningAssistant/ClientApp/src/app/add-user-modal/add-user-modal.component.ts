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
  selector: 'app-add-user-modal',
  templateUrl: './add-user-modal.component.html',
})
export class AddUserModal implements OnInit {
  constructor(public modal: NgbActiveModal, private _authService: AuthService,
    private _groupService: GroupService, public toastr: ToastrService) { }
  createdUser: User;

  roles: Role[] = [
    { text: 'Group headman', value: 'GroupHeadman' },
    { text: 'Speciality headman', value: 'SpecialityHeadman' },
    { text: 'Student', value: 'Student' }
  ];

  duplicateName = false;

  roleNameFormControl = new FormControl('', [
    Validators.required,
  ]);

  groupNumberFormControl = new FormControl('', [
    Validators.required,
  ]);

  subGroupNumberFormControl = new FormControl('', [
  ]);

  matcher = new MyErrorStateMatcher();

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