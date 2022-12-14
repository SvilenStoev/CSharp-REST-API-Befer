import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';

import { IUser } from '../../interfaces';
import { ApiService } from '../api.service';
import { StorageService } from '../common/storage.service';

export interface CreateUserDto { username: string, fullName: string, email: string, password: string }
export interface UserDataDto { username: string, id: string, token: string }
export interface UserEditDto { username: string, fullName: string, email: string, profilePicture: any }

@Injectable()
export class UserService {

  userColl: string = '/identity';

  get isLogged() {
    return !!this.storage.getUserData();
  }

  get userId(): string {
    return this.storage.getUserData()?.id;
  }

  get getToken(): string {
    return this.storage.getUserData()?.token;
  }

  constructor(private storage: StorageService, private api: ApiService) {

  }

  login$(data: { username: string, password: string }): Observable<any> {
    return this.api
      .post<any>(this.userColl + '/login', data)
      .pipe(
        map(response => response.body),
        tap(user => {
          const userData: UserDataDto = {
            username: user.username,
            id: user.objectId,
            token: user.sessionToken
          };

          this.storage.setUserData(userData);
        })
      );
  }

  logout$(): Observable<void> {
    return this.api.post<void>(this.userColl + '/logout');
  }

  register$(data: CreateUserDto): Observable<any> {
    return this.api
      .post<IUser>(this.userColl + '/register', data)
      .pipe(
        map(response => response.body),
        tap(user => {
          const userData: UserDataDto = {
            username: user.username,
            id: user.objectId,
            token: user.sessionToken
          };

          this.storage.setUserData(userData);
        })
      );
  }

  getMyProfile$(): Observable<any> {
    return this.api.get(`${this.userColl + '/profile'}`);
  }

  editProfile$(userData: UserEditDto): Observable<any> {
    // const formData = new FormData();
    // formData.set('username', userData.username);
    // formData.set('fullName', userData.fullName);
    // formData.set('email', userData.email);

    // if (userData.profilePicture) {
    //   formData.append('profilePicture', userData.profilePicture);
    // }

    // console.log(formData.get('profilePicture'));
    // const body = {
    //   "profilePicture":
    //   {
    //     "__type": "File",
    //     "name": formData.get('profilePicture')
    //   }
    // }

    // var parseFile = new Parse.File(name, file);

    return this.api.put<IUser>(`${this.userColl}`, userData);
  }
}