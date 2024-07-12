import { Component , Output , EventEmitter} from '@angular/core';
import { CheckboxComponent } from '../checkbox/checkbox.component';

@Component({
  selector: 'app-task-item',
  standalone: true,
  imports: [CheckboxComponent],
  templateUrl: './task-item.component.html',
  styleUrl: './task-item.component.scss'
})
export class TaskItemComponent {
  @Output() changeEvent = new EventEmitter<boolean>();
  onChange(checked : boolean) {
    this.changeEvent.emit(checked);
  }
}
