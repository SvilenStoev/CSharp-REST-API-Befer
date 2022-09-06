import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

import { IComment } from '../../interfaces';
import { ApiService } from '../api.service';
import { UserService } from '../auth/user.service';

export interface CreateCommentDto {
  content: string,
  postId: string
}

export interface CreateAuthorDto {
  __type: string,
  className: string,
  objectId: string,

}

export interface CreatePostDto {
  __type: string,
  className: string,
  objectId: string,
}

@Injectable()
export class CommentService {
  postColl: string = '/comments';

  constructor(private api: ApiService, private userService: UserService) { }

  loadPostComments$(postId: string): Observable<any> {
    return this.api.get(`${this.postColl}/${postId}`);
  }

  createComment$(commentData: CreateCommentDto, postId: string): Observable<IComment> {
    const userId = this.userService.userId;

    if (!userId || !postId) {
      throw new Error('Something went wrong!');
    }

    commentData.postId = postId;

    return this.api
      .post<IComment>(this.postColl, commentData)
      .pipe(
        map(response => response.body));
  }

  deleteComment$(postId: string): Observable<any> {
    return this.api.delete(`${this.postColl}/${postId}`);
  }

  editComment$(commentData: CreateCommentDto, postId: string, commentId: string): Observable<IComment> {
    const userId = this.userService.userId;

    if (!userId || !postId) {
      throw new Error('Something went wrong!');
    }

    return this.api
      .put<IComment>(`${this.postColl}/${commentId}`, commentData)
      .pipe(
        map(response => response.body));
  }
}
