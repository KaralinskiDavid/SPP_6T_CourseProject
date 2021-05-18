import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse, HttpResponse, HttpClient, HttpSentEvent, HttpHeaderResponse, HttpProgressEvent, HttpUserEvent } from '@angular/common/http';
import { Observable, throwError, Subject, of, BehaviorSubject } from 'rxjs';
import { catchError, map, tap, finalize, switchMap, filter, take } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  isRefreshingToken: boolean = false;
  tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);

  constructor(private authService: AuthService, private router: Router) { }

  addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({ setHeaders: { Authorization: 'Bearer ' + token } })
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
    let accessToken = localStorage.getItem('access_token') ? localStorage.getItem('access_token') : '';
    return next.handle(this.addToken(req, accessToken)).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          switch (error.status) {
            case 401:
              return this.handle401Error(req, next);
          }
        } else {
          return Observable.throw(error);
        }

      })) as Observable<HttpEvent<any>>;
  }

  handle401Error(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshingToken) {
      this.isRefreshingToken = true;

      // Reset here so that the following requests wait until the token
      // comes back from the refreshToken call.
      this.tokenSubject.next(null);

      let refreshToken = localStorage.getItem('refresh_token') ? localStorage.getItem('refresh_token') : '';

      return this.authService.refreshToken(refreshToken).pipe(
        switchMap((newToken: any) => {
          if (newToken['token']) {
            localStorage.setItem('access_token', newToken['token']);
            localStorage.setItem('refresh_token', newToken['refreshToken']);
            this.tokenSubject.next(newToken['token']);
            return next.handle(this.addToken(req, newToken['token']));
          } else {
            return this.logoutUser(req);
          }
        }),
        catchError(error => {
          return this.logoutUser(req);
        }),
        finalize(() => {
          this.isRefreshingToken = false;
        })
      );

    } else {
      if (req.url.indexOf("refresh")>0) {
        return this.logoutUser(req);
      }
      return this.tokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(token => {
          return next.handle(this.addToken(req, token));
        }));
    }
  }

  logoutUser(req) {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('userName');
    localStorage.removeItem('email');
    localStorage.removeItem('role');
    localStorage.removeItem('groupNumber');
    localStorage.removeItem('subGroup');

    window.location.replace('login');

    return throwError('');
  }


}
