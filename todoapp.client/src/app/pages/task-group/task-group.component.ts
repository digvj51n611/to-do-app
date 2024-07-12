import { Component, OnInit } from '@angular/core';
import { TaskItemComponent } from '../../components/task-item/task-item.component';
import { StatsCardComponent } from '../../components/stats-card/stats-card.component';
import { DatePipe } from '@angular/common';
import { TaskItem } from '../../data/models';
import { TaskService } from '../../../services/data/task/task.service';
import { map } from 'rxjs';

@Component({
  selector: 'app-task-group',
  standalone: true,
  imports: [TaskItemComponent,StatsCardComponent,],
  templateUrl: './task-group.component.html',
  styleUrl: './task-group.component.scss',
  providers :[DatePipe]
})
export class TaskGroupComponent implements OnInit {
  taskGroupHeader : string = "Today's Tasks";
  tempBoolean : boolean = false;
  tempNumber : number = 0.4;
  formattedDate : string | null ;
  taskList : TaskItem[] = [];
  elseComponentMessage : string = '';
  constructor(private datePipe : DatePipe,private service : TaskService ) {
    const date = new Date(); // Month is zero-based, so 11 is December
    this.formattedDate = this.datePipe.transform(date, 'EEEE, dd MMMM yyyy');    
  }
  ngOnInit(): void {
    this.service.getAllTasks().pipe(map(response => {
      if( response.isSuccess ) {
        this.taskList = response.data;
      }
      else {
        this.elseComponentMessage = 'No Tasks Found for Today';
      }
    }));
    
  }
}
