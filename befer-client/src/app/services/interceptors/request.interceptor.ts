import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

import { UserService } from '../auth/user.service';

@Injectable()
export class RequestInterceptor implements HttpInterceptor {

  constructor(private userService: UserService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.userService.getToken;

    if (token) {
      req = req.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}` 
        }
      });
    }

    return next.handle(req);
  };
}
