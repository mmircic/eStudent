import { Injectable } from '@angular/core';
import { CourseType } from '../models/course-type.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CourseTypeService {

  API_BASE_URL = environment.baseUri;

  constructor(private http: HttpClient) {
  }

  getAllCourseTypes(): any {
    return this.http.get(this.API_BASE_URL + 'coursetype/all');
  }
}
