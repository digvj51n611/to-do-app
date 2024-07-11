import { Component } from '@angular/core';
import { TaskGroupComponent } from '../task-group/task-group.component';
import { BannerComponent } from '../../components/banner/banner.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [TaskGroupComponent,BannerComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {

}
