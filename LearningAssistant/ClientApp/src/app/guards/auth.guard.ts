import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { JwtHelperService } from "@auth0/angular-jwt";

const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router, public jwtHelper: JwtHelperService) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    console.log(this.jwtHelper.isTokenExpired());
    this.authService.url = state['url'];
    if (localStorage.getItem('access_token')) {
      if (!this.jwtHelper.isTokenExpired()) {
        return true;
      } else {
        /*this.router.navigate(['/login']);
        return false;*/
        if (localStorage.getItem('refresh_token')) {
          this.authService.refreshToken(localStorage.getItem('refresh_token'))
            .subscribe((result: any) => {
              if (result) {
                localStorage.setItem('access_token', result['token']);
                localStorage.setItem('refresh_token', result['refreshToken']);
                this.router.navigate([this.authService.url]);
                return true;
              } else {
                localStorage.removeItem('access_token');
                localStorage.removeItem('refresh_token');
                localStorage.removeItem('email');
                localStorage.removeItem('userName');
                localStorage.removeItem('role');
                localStorage.removeItem('groupNumber');
                localStorage.removeItem('subGroup');
                this.router.navigate(['/login']);
                return false;
              }
            },
              error => {
                console.log(error);
                localStorage.removeItem('access_token');
                localStorage.removeItem('refresh_token');
                localStorage.removeItem('email');
                localStorage.removeItem('userName');
                localStorage.removeItem('role');
                localStorage.removeItem('groupNumber');
                localStorage.removeItem('subGroup');
                this.router.navigate(['/login']);
                return false;
              });

        } else {
          localStorage.removeItem('access_token');
          localStorage.removeItem('refresh_token');
          localStorage.removeItem('email');
          localStorage.removeItem('userName');
          localStorage.removeItem('role');
          localStorage.removeItem('groupNumber');
          localStorage.removeItem('subGroup');
          this.router.navigate(['/login']);
          return false;
        }
      }
    } else {
      localStorage.removeItem('access_token');
      localStorage.removeItem('refresh_token');
      localStorage.removeItem('userName');
      localStorage.removeItem('email');
      localStorage.removeItem('role');
      localStorage.removeItem('groupNumber');
      localStorage.removeItem('subGroup');
      this.router.navigate(['/login']);
      return false;
    }
  }

}
