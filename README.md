# xamarin-todo-with-realm




Xamarin.Formsの [Todoサンプル](https://developer.xamarin.com/samples/xamarin-forms/Todo/) をSQLite仕様から [Realm Xamarin](https://realm.io/docs/xamarin/latest/) 仕様に置き換えたもの。


## サンプルの改造点

各プロジェクトでNuGetパッケージを追加、更新。
(Xamarin.Forms 2.2.0、Realm 0.74.1)

PCLプロジェクトを修正。

**TodoItem.cs**

SQLite版ではIDプロパティをオートインクリメントにしていますが、現時点ではRealmがオートインクリメントに対応していないそうなので、intからstringに変更してGUIDを使うことにしました。

**TodoItemDatabase.cs**

基本的にSQLite DBの操作をRealmに置き換え。

ただし、現時点ではRealmへのLinqクエリでWherer等のサポートが不完全なため、いったんToList()してから改めてフィルタリングしています。

**App.cs**

TodoItem.IDをstring型に変更した関係でApp.csも一部修正。

**Views/TodoItemListX.xaml.cs**

SQLite版を踏襲すると  TodoItemListX.xaml.csでTodo編集ページのBindingContextにTodoItemを渡すことになります。
そのまままでは、双方向BindingでプロパティSetterが呼ばれて死ぬので一工夫必要です。
(Realmが管理中のRealmObjectはトランザクション外での編集禁止)

今回は編集用のコピーを作ってBindingContextにセットしています。


## 元のTodoサンプルのライセンス

> The Apache License 2.0 applies to all samples in this repository.
> 
> Copyright 2011 Xamarin Inc
> 
> Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
> 
> >   http://www.apache.org/licenses/LICENSE-2.0
> 
> Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.



