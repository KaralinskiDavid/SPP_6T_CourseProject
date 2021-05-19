import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../classes/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private urla = "/api/auth";

  private redirectURL: string = "";

  public set url(value: string) {
    this.redirectURL = value;
  }

  public get url() {
    return this.redirectURL;
  }

  constructor(private http: HttpClient) { }

  signIn(email: string, password: string) {
    const user = { email: email, password: password };
    return this.http.post(this.urla + '/login', user);
  }

  signUp(user: User) {
    return this.http.post(this.urla + '/register', user);
  }

  checkEmail(email: string) {
    return this.http.get(this.urla + '/check/' + email);
  }

  refreshToken(token: string) {
    const rToken = { refreshToken: token };
    return this.http.post(this.urla + "/refresh", rToken);
  }

}
