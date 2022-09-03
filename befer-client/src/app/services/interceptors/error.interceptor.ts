import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';

import { notifyErr } from 'src/app/shared/other/notify';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        catchError(err => {
          if (!err.error.title) {
            throw {
              message: err.message,
            };
          } else if (err.error.status == 404) {
            this.router.navigate(['NotFound']);
          } else {
            notifyErr(err.error.title);
          }

          //Error from server
          throw {
            message: err.error.title,
            code: err.error.status
          };
        }));
  }
}