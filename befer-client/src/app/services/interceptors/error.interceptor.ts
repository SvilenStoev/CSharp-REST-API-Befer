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
          if (!err.error.title && !err.error[0]) {
            throw {
              message: err.message,
            };
          } else if (err.error.status == 404) {
            this.router.navigate(['NotFound']);
          } else if (err.error.title) {
            notifyErr(err.error.title);
          }

          //Error from server
          if (err.error[0].code == 'DuplicateUserName') {
            notifyErr(err.error[0].description);

            throw {
              message: err.error[0].description,
              code: 202
            };
          } else {
            throw {
              message: err.error.title,
              code: err.error.status
            }
          };
        }));
  }
}