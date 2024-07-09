import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { AuthCardComponent } from './components/auth-card/auth-card.component';
import { AuthenticationComponent } from './pages/authentication/authentication.component';
import { ModalComponent } from './components/modal/modal.component';
import { SideBarComponent } from './layouts/side-bar/side-bar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet,SideBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'todoapp.client';
}
