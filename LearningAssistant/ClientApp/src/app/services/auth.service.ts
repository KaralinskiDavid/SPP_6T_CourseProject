import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../classes/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private url = "/api/auth";

  constructor(private http: HttpClient) { }

  signIn(email: string, password: string) {
    const user = { email: email, password: password };
    return this.http.post(this.url + '/login', user);
  }

  signUp(user: User) {
    return this.http.post(this.url + '/register', user);
  }

  checkEmail(email: string) {
    return this.http.get(this.url + '/check/' + email);
  }

  refreshToken(token: string) {
    return this.http.post(this.url + "/refresh/" + token, null);
  }

}
