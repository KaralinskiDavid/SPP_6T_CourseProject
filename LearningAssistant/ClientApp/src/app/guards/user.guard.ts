import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (localStorage.getItem('role') == 'Student' || localStorage.getItem('role') == 'GroupHeadman' || localStorage.getItem('role') == 'SpecialityHeadman') {
      return true;
    } else {
      this.router.navigate(['']);
      return false;
    }
  }

}
