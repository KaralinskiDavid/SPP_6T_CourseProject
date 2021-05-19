import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (localStorage.getItem('role') == "Admin") {
      this.router.navigate(['/admin']);
      return false;
    } else if (localStorage.getItem('role')) {
      this.router.navigate(['/user']);
      return false;
    }
    else
      return true;
  }

}
