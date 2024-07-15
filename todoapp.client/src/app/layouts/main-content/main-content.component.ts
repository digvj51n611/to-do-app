import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { RouterOutlet } from '@angular/router';
import { TaskGroupComponent } from '../../pages/task-group/task-group.component';
import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { ActiveComponent } from '../../pages/active/active.component';

@Component({
  selector: 'app-main-content',
  standalone: true,
  imports: [HeaderComponent,RouterOutlet,DashboardComponent,ActiveComponent],
  templateUrl: './main-content.component.html',
  styleUrl: './main-content.component.scss'
})
export class MainContentComponent {

}
