import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

import { ApiService } from '../../api.service';

export interface UserScoresDto {
  aliensKilled: number,
  aliensMissed: number,
  timeRemaining: number,
  boostRemaining: number,
  points: number,
  totalPoints: number,
  healthRemaining: number,
  username?: string
}

@Injectable()
export class GameApiService {

  postColl: string = '/gameScores';

  constructor(private api: ApiService) { }

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
