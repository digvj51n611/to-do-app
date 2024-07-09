import { NgClass } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-auth-card',
  standalone: true,
  imports: [NgClass],
  templateUrl: './auth-card.component.html',
  styleUrl: './auth-card.component.scss'
})
export class AuthCardComponent {
  isRegister : Boolean = false;
}
