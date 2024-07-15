import { Component } from '@angular/core';
import { TaskGroupComponent } from '../task-group/task-group.component';
import { taskGroupMode } from '../../data/enums';

@Component({
  selector: 'app-active',
  standalone: true,
  imports: [TaskGroupComponent],
  templateUrl: './active.component.html',
  styleUrl: './active.component.scss'
})
export class ActiveComponent {
  taskMode : taskGroupMode = taskGroupMode.active;
}
