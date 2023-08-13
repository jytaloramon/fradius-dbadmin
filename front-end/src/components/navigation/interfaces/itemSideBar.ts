interface IItemSidebarBase {
  id: number;
  items?: IItemSideBarShort[];
}

export interface IItemSideBarShort {
  label: string;
  icon: string;
  path: string;
}

export interface IItemSideBarComum extends IItemSidebarBase, IItemSideBarShort {}

export interface IItemSideBarWithSubmenu extends IItemSidebarBase {
  label: string;
  icon: string;
}
