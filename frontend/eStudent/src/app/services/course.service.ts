import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Course } from '../models/course.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  
    
  API_BASE_URL = environment.baseUri;
  Course: Course;
  
  constructor(private http: HttpClient) { }

  getAllCourses(): any {
    return this.http.get(this.API_BASE_URL + 'course/all');
  }

  deleteCourse(id: number): Observable<{}> {
    return this.http.delete(this.API_BASE_URL + 'course/' + id);
  }

  updateCourse(id: number, course: any): any {
    return this.http.put(this.API_BASE_URL + 'course/' + id, course,{
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  createCourse(course: any): any {
    return this.http.post(this.API_BASE_URL + 'course', course,{
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  sendRequest(courseId: number, userId: number): any {
    return this.http.post(this.API_BASE_URL + 'usercourse', {'userId': userId, 'courseId': courseId});
  }
  
  getAllUnacceptedRequests(): any{
    return this.http.get(this.API_BASE_URL + 'usercourse/unaccepted/all');
  }

  deleteRequest(id: number): any {
    return this.http.delete(this.API_BASE_URL + 'usercourse/' + id);
  }
  acceptRequest(id: number): any {
    return this.http.put(this.API_BASE_URL + 'usercourse/' + id, {'accepted': true});
  }

  getAllCurrentStudents(): any {
    return this.http.get(this.API_BASE_URL + 'usercourse/accepted/all');
  }
 
}
