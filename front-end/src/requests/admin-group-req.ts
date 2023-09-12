export interface IAdminGroup {
  id: number;
  name: string;
  rules: number[]
}

export interface IAdminGroupShort {
  id: number;
  name: string;
  qtUsers: number;
}

export const adminGroupGetAll = (): IAdminGroupShort[] => {
  return [
    { id: 1, name: 'Group 1', qtUsers: 150 },
    { id: 2, name: 'Group 2', qtUsers: 1 },
    { id: 3, name: 'Group 3', qtUsers: 50 },
    { id: 4, name: 'Group 1', qtUsers: 15 },
    { id: 5, name: 'Group 2', qtUsers: 1 },
    { id: 6, name: 'Group 3', qtUsers: 50 },
    { id: 7, name: 'Group 1', qtUsers: 15 },
    { id: 8, name: 'Group 2', qtUsers: 1 }];
};

export const adminGroupPost = (group: { name: string; rules: number[] }): IAdminGroup => {

  console.log(group);

  return { id: Math.ceil(Math.random() * 10000), name: group.name, rules: group.rules };
}