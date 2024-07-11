import { Component } from '@angular/core';
import { TaskItemComponent } from '../../components/task-item/task-item.component';
import { StatsCardComponent } from '../../components/stats-card/stats-card.component';

@Component({
  selector: 'app-task-group',
  standalone: true,
  imports: [TaskItemComponent,StatsCardComponent],
  templateUrl: './task-group.component.html',
  styleUrl: './task-group.component.scss'
})
export class TaskGroupComponent {
  taskGroupHeader : string = "Today's Tasks";
  tempBoolean : boolean = false;
  tempNumber : number = 0.4;
}
