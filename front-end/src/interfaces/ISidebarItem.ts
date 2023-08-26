interface IItem {
  key: string;
  icon: string;
}

interface IItemSidebarBase {
  items?: IItemSideBarShort[];
}

interface IItemSideBarShort extends IItem {
  path: string;
}

export interface IItemSideBarComum extends IItemSidebarBase, IItemSideBarShort {}

export interface IItemSideBarWithSubmenu extends IItemSidebarBase, IItem {}
