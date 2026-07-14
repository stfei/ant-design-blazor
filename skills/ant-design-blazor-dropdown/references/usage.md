# Ant Design Blazor Dropdown 多种用法

以下代码已复制到技能内部。先根据差异表选择最接近需求的模式，再只保留该模式真正需要的参数、数据和回调。

## 用法索引

| 用法 | 关键差异 |
| --- | --- |
| 基础用法 | 从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。 |
| 触发事件 | 点击菜单项后会触发事件，用户可以通过相应的菜单项 key 进行不同的操作。 |
| 右键菜单(目前只有NET5完整支持) | 默认是移入触发菜单，可以点击鼠标右键触发。(目前只有NET5完整支持) |
| 触发方式 | 默认是移入触发菜单，可以点击触发。 |
| 分组与子菜单 | 在 `Menu` 内组合 `MenuItemGroup` 与 `SubMenu`，表达分组和多级操作。 |
| 菜单隐藏方式 | 默认是点击关闭菜单，可以关闭此功能。 |

## 1. 基础用法

关键差异：从最少参数开始，展示组件的核心标签、主要绑定以及必要的数据或回调。

```razor
<Dropdown>
    <Overlay>
        <Menu>
            <MenuItem OnClick="Edit">编辑</MenuItem>
            <MenuItem Danger OnClick="Delete">删除</MenuItem>
        </Menu>
    </Overlay>
    <ChildContent>
        <Button>更多 <Icon Type="@IconType.Outline.Down" /></Button>
    </ChildContent>
</Dropdown>

@code {
    private void Edit() { }
    private void Delete() { }
}
```

## 2. 触发事件

关键差异：点击菜单项后会触发事件，用户可以通过相应的菜单项 key 进行不同的操作。

```razor
<Dropdown>
    <Overlay>
        <Menu>
            <MenuItem @key="1">1st menu item</MenuItem>
            <MenuItem @key="2">2nd memu item</MenuItem>
            <MenuItem @key="3">3rd menu item</MenuItem>
        </Menu>
    </Overlay>
    <ChildContent>
        <a class="ant-dropdown-link" @onclick:preventDefault>
            Hover me, Click menu item <Icon Type="@IconType.Outline.Down" />
        </a>
    </ChildContent>
</Dropdown>
```

## 3. 右键菜单(目前只有NET5完整支持)

关键差异：默认是移入触发菜单，可以点击鼠标右键触发。(目前只有NET5完整支持)

```razor
<Dropdown Trigger="new Trigger[] { Trigger.ContextMenu }">
    <Overlay>
        <Menu>
            <MenuItem>1st menu item</MenuItem>
            <MenuItem>2nd menu item</MenuItem>
            <MenuItem>3rd menu item</MenuItem>
        </Menu>
    </Overlay>
    <ChildContent>
        <div style="text-align: center; width: 200px; height: 200px; line-height: 200px;background: #f7f7f7;color: #777;">
            Right Click on here
        </div>
    </ChildContent>
</Dropdown>
```

## 4. 触发方式

关键差异：默认是移入触发菜单，可以点击触发。

```razor
<Dropdown Trigger="@(new Trigger[] { Trigger.Click })">
    <Overlay>
        <Menu>
            <MenuItem>
                <a target="_blank" rel="noopener noreferrer" href="http://www.alipay.com/">
                    1st menu item
                </a>
            </MenuItem>
            <MenuItem>
                <a target="_blank" rel="noopener noreferrer" href="http://www.taobao.com/">
                    2nd menu item
                </a>
            </MenuItem>
            <MenuItem>
                <a target="_blank" rel="noopener noreferrer" href="http://www.tmall.com/">
                    3rd menu item
                </a>
            </MenuItem>
        </Menu>
    </Overlay>
    <ChildContent>
        <a class="ant-dropdown-link" @onclick:preventDefault>
            Click me <Icon Type="@IconType.Outline.Down" />
        </a>
    </ChildContent>
</Dropdown>
```

## 5. 分组与子菜单

关键差异：在 `Menu` 内组合 `MenuItemGroup` 与 `SubMenu`，表达分组和多级操作。

```razor
<Dropdown>
    <Overlay>
        <Menu>
            <MenuItemGroup Title="Group Title">
                <MenuItem>1st menu item</MenuItem>
                <MenuItem>2nd menu item</MenuItem>
            </MenuItemGroup>
            <SubMenu Title="Submenu">
                <MenuItem>3rd menu item</MenuItem>
                <MenuItem>4th menu item</MenuItem>
            </SubMenu>
            <SubMenu Title="Disabled Submenu" Disabled>
                <MenuItem>5th menu item</MenuItem>
                <MenuItem>6th menu item</MenuItem>
            </SubMenu>
        </Menu>
    </Overlay>
    <ChildContent>
        <a class="ant-dropdown-link" @onclick:preventDefault>
            Cascading Menu <Icon Type="@IconType.Outline.Down" />
        </a>
    </ChildContent>
</Dropdown>
```

## 6. 菜单隐藏方式

关键差异：默认是点击关闭菜单，可以关闭此功能。

```razor
<Dropdown @ref="@_dropdown">
    <Overlay>
        <Menu OnMenuItemClicked="HandleMenuClick" AutoCloseDropdown="@false">
            <MenuItem Id="1">Clicking me will not close the menu.</MenuItem>
            <MenuItem Id="2">Clicking me will not close the menu also.</MenuItem>
            <MenuItem Id="3">Clicking me will close the menu.</MenuItem>
        </Menu>
    </Overlay>
    <ChildContent>
        <a class="ant-dropdown-link" @onclick:preventDefault>
            Hover me <Icon Type="@IconType.Outline.Down" />
        </a>
    </ChildContent>
</Dropdown>

@code
{
    private Dropdown _dropdown;

    private async Task HandleMenuClick(MenuItem item)
    {
        if (item.Id == "3")
        {
           await _dropdown.Close();
        }
    }
}
```
