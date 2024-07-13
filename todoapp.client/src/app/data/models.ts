import { TaskStatus } from "./enums"

export interface TaskItem {
    taskId : number
    title : string
    description : string 
    timeAdded : Date
    taskStatus : TaskStatus
};
export interface Response<T> {
    isSuccess : boolean 
    message : string
    data : T
    errorList : Error[]
};