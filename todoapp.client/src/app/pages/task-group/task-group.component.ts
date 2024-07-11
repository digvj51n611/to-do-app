import { Component } from '@angular/core';
import { TaskItemComponent } from '../../components/task-item/task-item.component';
import { StatsCardComponent } from '../../components/stats-card/stats-card.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-task-group',
  standalone: true,
  imports: [TaskItemComponent,StatsCardComponent,],
  templateUrl: './task-group.component.html',
  styleUrl: './task-group.component.scss',
  providers :[DatePipe]
})
export class TaskGroupComponent {
  taskGroupHeader : string = "Today's Tasks";
  tempBoolean : boolean = false;
  tempNumber : number = 0.4;
  formattedDate : string | null ;
  /**
   *
   */
  constructor(private datePipe : DatePipe) {
    const date = new Date(); // Month is zero-based, so 11 is December
    this.formattedDate = this.datePipe.transform(date, 'EEEE, dd MMMM yyyy');    
  }
}
