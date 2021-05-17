import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

  email: string;
  password: string;
  loginForm: FormGroup;

  passwordFormControl = new FormControl('', [
    Validators.required,
  ]);

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  matcher = new MyErrorStateMatcher();

  constructor(private toastr: ToastrService, public router: Router, private authService: AuthService) {
    this.email = '';
    this.password = '';
    this.loginForm = new FormGroup({
      "email": new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      "password": new FormControl("", Validators.required)
    });
  }

  tryLogin() {
    this.authService.signIn(this.email, this.password).subscribe((result: any) => {
      this.toastr.success("Logged in successful");
      localStorage.setItem('access_token', result['token']);
      localStorage.setItem('refresh_token', result['refreshToken']);
      localStorage.setItem('email', result['email']);
      localStorage.setItem('userName', result['userName']);
      localStorage.setItem('role', result['role']);
    },
      error => {
        this.toastr.error("Wrong email or password");
      }
    );
  }

  ngOnInit() {
  }

}
