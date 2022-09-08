import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

import { IPost } from '../../interfaces';
import { ApiService } from '../api.service';
import { LanguageService } from '../common/language.service';

export interface CreatePostDto {
  afterImgUrl: string,
  beforeImgUrl: string,
  description: string,
  title: string,
  isPublic: boolean,
}

@Injectable()
export class PostService {

  postColl: string = '/posts';
  likeColl: string = '/likes';
  menuPostsHome: any = this.langService.get().postsHome;
  onlyPublic: string = JSON.stringify({
    isPublic: true
  });

  constructor(
    private api: ApiService,
    private langService: LanguageService) { }

  getAllPublicPostsCount$(): Observable<any> {
    return this.api.get(`${this.postColl + '/allPostsCount'}`);
  }

  getMyPostsCount$(): Observable<any> {
    return this.api.get(`${this.postColl + '/userPostsCount'}`);
  }

  loadAllPosts$(sortType: string, skipPosts: number, limit?: number): Observable<any> {
    sortType = this.getSortType(sortType);

    return this.api.get(`${this.postColl + '/getAll'}/?order=${sortType}&skip=${skipPosts}${limit ? `&limit=${limit}` : '&limit=0'}`);
  }

  loadMyPosts$(sortType: string, skipPosts: number): Observable<any> {
    sortType = this.getSortType(sortType);

    return this.api.get(`${this.postColl + '/getMine'}/?order=${sortType}&skip=${skipPosts}`);
  }

  loadPostById$(id: string): Observable<any> {
    return this.api.get(`${this.postColl + '/details'}/${id}`);
  }

  createPost$(postData: CreatePostDto): Observable<IPost> {
     return this.api
       .post<IPost>(this.postColl, postData)
       .pipe(
         map(response => response.body));
  }

  editPost$(postData: CreatePostDto, id: string): Observable<IPost> {
    return this.api.put<IPost>(`${this.postColl}/${id}`, postData);
  }

  deletePost$(id: string): Observable<any> {
    return this.api.delete(`${this.postColl}/${id}`);
  }

  likeByPostId$(postId: string): Observable<any> {
    return this.api
      .post<any>(`${this.likeColl}/${postId}`);
  }

  dislikeByPostId$(postId: string): Observable<any> {
    return this.api
      .delete(`${this.likeColl}/${postId}`);
  }

  getSortType(sortType: string): string {
    if (sortType == this.menuPostsHome.date) {
      return '-createdAt';
    } else {
      return '-likes';
    }
  }
}