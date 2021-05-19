import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap'
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm, ValidatorFn, AbstractControl } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { User } from '../classes/user';
import { GroupService } from '../services/group.service';
import { Group } from '../classes/iismodels';


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
    private _groupService: GroupService, public toastr: ToastrService) {
  }
  createdUser: User;
  currentGroup: Group;


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
        else if(this.emailFormControl.invalid)
          this.emailFormControl.errors['incorrect'] = null;
      });
    }
  }

  checkGroupNumber() {
    if (this.createdUser.groupNumber != null && this.createdUser.groupNumber.length > 0)
      this._groupService.checkGroupNumber(this.createdUser.groupNumber).subscribe((result: boolean) => {
        if (!result)
          this.groupNumberFormControl.setErrors({ incorrect: true });
        else if(this.groupNumberFormControl.invalid)
          this.groupNumberFormControl.errors['incorrect'] = null;
        else
          this._groupService.getGroupByNumber(this.createdUser.groupNumber).subscribe((result: Group) => {
            this.currentGroup = result;
          })
      });
  }

  checkRole() {
    if (this.currentGroup) {
      if ((this.roleNameFormControl.value == "GroupHeadman" && this.currentGroup.headStudentId != null) ||
        (this.roleNameFormControl.value == "SpecialityHeadman" && this.currentGroup.speciality.headStudentId != null))
        this.roleNameFormControl.setErrors({ alreadyExist: true });
      else if (this.roleNameFormControl.invalid)
        this.roleNameFormControl.errors['alreadyExist'] = null;
    }
    else if (this.roleNameFormControl.invalid)
      this.roleNameFormControl.errors['alreadyExist'] = null;
  }

  ngOnInit() {
    this.createdUser = new User();
  }
}
