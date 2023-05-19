import { Task } from "../tasks/task";

export interface CategoryTaskDetails {
    id: number;
    name: string;
    toDoItems: Task[];
  }