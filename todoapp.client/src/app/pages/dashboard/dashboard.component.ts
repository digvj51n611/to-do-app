import { Component } from '@angular/core';
import { TaskGroupComponent } from '../task-group/task-group.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [TaskGroupComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  
}
