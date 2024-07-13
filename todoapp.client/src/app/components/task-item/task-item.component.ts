import { Component , Output , EventEmitter, Input} from '@angular/core';
import { CheckboxComponent } from '../checkbox/checkbox.component';
import { TaskItem } from '../../data/models';
import { TaskState } from 'zone.js/lib/zone-impl';
import { TaskStatus } from '../../data/enums';

@Component({
  selector: 'app-task-item',
  standalone: true,
  imports: [CheckboxComponent],
  templateUrl: './task-item.component.html',
  styleUrl: './task-item.component.scss'
})
export class TaskItemComponent {
  @Input({required : true }) task : TaskItem | null  = null;
  @Output() changeEvent = new EventEmitter<boolean>();
  onChange(checked : boolean) {
    this.changeEvent.emit(checked);
  }
  isComplete(taskStatus : TaskStatus | undefined) {
    if( taskStatus == undefined ) return false;
    return taskStatus === TaskStatus.completed;
  }
}
