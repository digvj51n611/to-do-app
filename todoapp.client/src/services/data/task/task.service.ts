import { Injectable } from '@angular/core';
import { HttpService } from '../../http/http.service';
import { Response, TaskItem } from '../../../app/data/models';
import { Observable,map} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  constructor(private httpService : HttpService) {
  }
  getAllTasks() : Observable<Response<TaskItem>> {
    return this.httpService.getAll<TaskItem>();
  }
  getTask(id : number ) : Observable<Response<TaskItem>>{
    return this.httpService.get<TaskItem>(id);
  }
  updateTask(id : number, task : TaskItem ) : Observable<Response<TaskItem>>{
    return this.httpService.update<TaskItem>(id , task );
  }
  createTask(task : TaskItem) : Observable<Response<TaskItem>> {
    return this.httpService.create<TaskItem>(task);
  }
  deleteTask(id : number) : Observable<Response<TaskItem>> {
    return this.httpService.delete<TaskItem>(id);
  }
}
