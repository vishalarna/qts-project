export class TaskTree {
    id: any;
    description: string;
    children: TaskTree[] = [];
    active: boolean;
    parent: TaskTree;
    level: string;
}