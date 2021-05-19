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
  selector: 'app-edit-user-modal',
  templateUrl: './edit-user-modal.component.html',
  styleUrls: ['./edit-user-modal.component.css']
})
export class EditUserModalComponent implements OnInit {

  constructor(public modal: NgbActiveModal, private _authService: AuthService,
    private _groupService: GroupService, public toastr: ToastrService) { }
  editedUser: User;
  currentGroup: Group;

  roles: Role[] = [
    { text: 'Group headman', value: 'GroupHeadman' },
    { text: 'Speciality headman', value: 'SpecialityHeadman' },
    { text: 'Student', value: 'Student' }
  ];

  duplicateName = false;
  firstRole: string;

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
    if (this.editedUser.groupNumber != null && this.editedUser.groupNumber.length > 0)
      this._groupService.checkGroupNumber(this.editedUser.groupNumber).subscribe((result: boolean) => {
        if (!result)
          this.groupNumberFormControl.setErrors({ incorrect: true });
        else if (this.groupNumberFormControl.invalid)
          this.groupNumberFormControl.errors['incorrect'] = null;
        else
          this._groupService.getGroupByNumber(this.editedUser.groupNumber).subscribe((result: Group) => {
            this.currentGroup = result;
          })
      });
  }

  checkRole() {
    if (this.currentGroup) {
      if ((this.roleNameFormControl.value == "GroupHeadman" && this.currentGroup.headStudentId != null && this.editedUser.roleName != this.firstRole) ||
        (this.roleNameFormControl.value == "SpecialityHeadman" && this.currentGroup.speciality.headStudentId != null && this.editedUser.roleName != this.firstRole))
        this.roleNameFormControl.setErrors({ alreadyExist: true });
      else if (this.roleNameFormControl.invalid)
        this.roleNameFormControl.errors['alreadyExist'] = null;
    }
    else if (this.roleNameFormControl.invalid)
      this.roleNameFormControl.errors['alreadyExist'] = null;
  }

  ngOnInit() {
    this.firstRole = this.editedUser.roleName;
    this.roleNameFormControl.setValue(this.firstRole);
    this._groupService.checkGroupNumber(this.editedUser.groupNumber).subscribe((result: boolean) => {
      if (!result)
        this.groupNumberFormControl.setErrors({ incorrect: true });
      else if (this.groupNumberFormControl.invalid)
        this.groupNumberFormControl.errors['incorrect'] = null;
      else
        this._groupService.getGroupByNumber(this.editedUser.groupNumber).subscribe((result: Group) => {
          this.currentGroup = result;
        })
    });
  }

}
