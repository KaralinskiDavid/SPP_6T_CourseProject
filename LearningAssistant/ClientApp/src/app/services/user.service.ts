import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../classes/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private url = "/api/users";

  constructor(private http: HttpClient) { }

  deleteUser(userId: string) {
    return this.http.delete(this.url + "/" + userId);
  }

  updateUser(userId: string, user: User) {
    return this.http.put(this.url + "/" + userId, user);
  }

}
