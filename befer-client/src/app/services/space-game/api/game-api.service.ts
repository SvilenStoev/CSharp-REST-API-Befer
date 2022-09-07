import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

import { ApiService } from '../../api.service';
import { UserService } from '../../auth/user.service';

export interface UserScoresDto {
  aliensKilled: number,
  timeRemaining: number,
  boostRemaining: number,
  points: number,
  totalPoints: number,
  player: CreatePlayerDto,
}

export interface CreatePlayerDto {
  __type: string,
  className: string,
  objectId: string,
}

@Injectable()
export class GameApiService {

  postColl: string = '/gameScores';

  constructor(private api: ApiService, private userService: UserService) { }

  createScores$(userScores: UserScoresDto): Observable<any> {
    return this.api
      .post<any>(this.postColl, userScores)
      .pipe(
        map(response => response.body));
  }

  loadMyScores$(): Observable<any> {
    return this.api.get(`${this.postColl}/getMine`);
  }

  loadAllScores$(): Observable<any> {
    return this.api.get(`${this.postColl}/getAll`);
  }

  updateScores$(userScores: UserScoresDto, id: string): Observable<any> {
    return this.api
      .put<any>(`${this.postColl}/${id}`, userScores)
      .pipe(
        map(response => response.body));
  }
}
