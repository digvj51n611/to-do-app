import { Component, OnInit , Input} from '@angular/core';
import { TaskItemComponent } from '../../components/task-item/task-item.component';
import { StatsCardComponent } from '../../components/stats-card/stats-card.component';
import { DatePipe, NgFor, NgIf } from '@angular/common';
import { TaskItem } from '../../data/models';
import { TaskService } from '../../../services/data/task/task.service';
import { TaskStatus, taskGroupMode } from '../../data/enums';

@Component({
  selector: 'app-task-group',
  standalone: true,
  imports: [TaskItemComponent,StatsCardComponent,NgFor,NgIf],
  templateUrl: './task-group.component.html',
  styleUrl: './task-group.component.scss',
  providers :[DatePipe]
})
export class TaskGroupComponent implements OnInit {
  @Input() taskGroupMode : taskGroupMode = taskGroupMode.all;
  taskGroupHeader! : string ;
  tempNumber : number = 0.4;
  formattedDate : string | null;
  taskList : TaskItem[] = [ 
    { taskId: 1, title: 'Task 1', description: 'Description for task 1', timeAdded: new Date('2023-01-01T08:00:00'), taskStatus: TaskStatus.pending },
    { taskId: 2, title: 'Task 2', description: 'Description for task 2', timeAdded: new Date('2023-02-01T09:00:00'), taskStatus: TaskStatus.completed },
    { taskId: 3, title: 'Task 3', description: 'Description for task 3', timeAdded: new Date('2023-03-01T10:00:00'), taskStatus: TaskStatus.completed },
    { taskId: 4, title: 'Task 4', description: 'Description for task 4', timeAdded: new Date('2023-04-01T11:00:00'), taskStatus: TaskStatus.pending },
    { taskId: 5, title: 'Task 5', description: 'Description for task 5', timeAdded: new Date('2023-05-01T12:00:00'), taskStatus: TaskStatus.pending },
    { taskId: 6, title: 'Task 6', description: 'Description for task 6', timeAdded: new Date('2023-06-01T13:00:00'), taskStatus: TaskStatus.completed },
    { taskId: 7, title: 'Task 7', description: 'Description for task 7', timeAdded: new Date('2023-07-01T14:00:00'), taskStatus: TaskStatus.pending },
    { taskId: 8, title: 'Task 8', description: 'Description for task 8', timeAdded: new Date('2023-08-01T15:00:00'), taskStatus: TaskStatus.pending },
    { taskId: 9, title: 'Task 9', description: 'Description for task 9', timeAdded: new Date('2023-09-01T16:00:00'), taskStatus: TaskStatus.completed },
    { taskId: 10, title: 'Task 10', description: 'Description for task 10', timeAdded: new Date('2023-10-01T17:00:00'), taskStatus: TaskStatus.completed },
  ];
  elseComponentMessage : string = '';
  constructor(private datePipe : DatePipe,private service : TaskService ) {
    const date = new Date(); 
    this.formattedDate = this.datePipe.transform(date, 'EEEE, dd MMMM yyyy');   
  }
  ngOnInit(): void {
    this.service.getAllTasks()
    .subscribe((response) =>{
      if( response.isSuccess ) {
        this.taskList = response.data;
      }
      else {
        this.elseComponentMessage = 'No Tasks Found for Today';
      }
    });
    this.setTaskHeader();
  }
  onChange(checked : boolean , index : number ): void {
    this.taskList[index].taskStatus = checked? TaskStatus.completed : TaskStatus.pending;
    console.log( this.taskGroupMode);
  }
  isStatusComplete(status : TaskStatus ): boolean {
    return status === TaskStatus.completed;
  }
  isModeDashBoard() {
    return this.taskGroupMode == taskGroupMode.all
  }
  setTaskHeader() {
    this.taskGroupHeader = `Today's ${this.taskGroupMode.toString()} Tasks`; 
  }
}
