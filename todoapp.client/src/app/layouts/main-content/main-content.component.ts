import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { RouterOutlet } from '@angular/router';
import { TaskGroupComponent } from '../../pages/task-group/task-group.component';
import { DashboardComponent } from '../../pages/dashboard/dashboard.component';

@Component({
  selector: 'app-main-content',
  standalone: true,
  imports: [HeaderComponent,RouterOutlet,DashboardComponent],
  templateUrl: './main-content.component.html',
  styleUrl: './main-content.component.scss'
})
export class MainContentComponent {

}
