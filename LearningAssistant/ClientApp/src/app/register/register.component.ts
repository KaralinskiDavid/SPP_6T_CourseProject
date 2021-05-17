import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../classes/user';
import { AuthService } from '../services/auth.service';
import { GroupService } from '../services/group.service';

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  private emailValidator(control: FormControl, duplicateName: boolean): ValidationErrors {
    if (duplicateName) {
      return { forbiddenName: 'Error' };
    }
    return null;
  }

  user: User;
  registerForm: FormGroup;
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

  confirmPasswordFormControl = new FormControl('', [
    Validators.required,
  ]);

  groupNumberFormControl = new FormControl('', [
    Validators.required,
  ]);

  subGroupNumberFormControl = new FormControl('', [
  ]);

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  matcher = new MyErrorStateMatcher();

  constructor(private _authService: AuthService, private _groupService: GroupService, public router: Router, public toastr: ToastrService) {
    this.user = new User();
  }

  ngOnInit(): void {
  }

  tryRegister() {
    this._authService.signUp(this.user).subscribe((result: any) => {
      this.toastr.success('Registered');
      this.router.navigate(['/login']);
    },
      error => {
        console.log(error);
        this.toastr.error("Something went wrong");
      }
    );
  }

  checkEmail() {
    if (this.user.email != null && this.user.email.length > 0) {
      this._authService.checkEmail(this.user.email).subscribe((result: boolean) => {
        this.duplicateName = result;
        if (result)
          this.emailFormControl.setErrors({ incorrect: true });
        else
          this.emailFormControl.setErrors(null);
      });
    }
  }

  checkConfirm() {
    let confirm = this.confirmPasswordFormControl.value;
    if (confirm != null && confirm.length > 0) {
      if (this.passwordFormControl.value != confirm) {
        this.confirmPasswordFormControl.setErrors({ incorrect: true });
      }
      else {
        this.confirmPasswordFormControl.setErrors(null);
      }
    }
  }

  checkGroupNumber() {
    if(this.user.groupNumber!=null && this.user.groupNumber.length>0)
    this._groupService.checkGroupNumber(this.user.groupNumber).subscribe((result: boolean) => {
      if (!result)
        this.groupNumberFormControl.setErrors({ incorrect: true });
      else
        this.groupNumberFormControl.setErrors(null);
    });
  }

}
