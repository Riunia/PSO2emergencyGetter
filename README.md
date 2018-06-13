# PSO2emergencyGetter
[PSO2予告緊急API](https://github.com/aki-lua87/PSO2emaAPI)で取得した緊急クエスト情報と[覇者の紋章API](https://github.com/aki-lua87/PSO2CoatOfArms)で取得した情報をPostgreSQLサーバに書き込むアプリケーション

# 使い方
起動するとPostgreSQLサーバのアドレスとデータベースとユーザー名とパスワードを聞かれるのでそれを入力してPostgreSQLサーバに情報を書き込むコマンド(詳細は後に記述)を実行。

設定ファイルを書いてその場所を引数にして起動すると、データベースのアドレスなどを省略するこことができる。

また、コマンドを手動で実行しなくても、水曜日の17時になると自動で覇者の紋章・緊急クエストの情報を取得します。

# 設定ファイルの書き方
```shell
server = [サーバのアドレス]
database = [データベース名]
user = [ユーザー名]
password = [パスワード]
init = [true/false]
```
initはtrueだと初期化コマンドを実行して緊急クエスト情報と覇者の紋章の情報をデータベースに書き込む。

# コマンド
データベースに覇者の紋章または緊急クエストのテーブルを作成
```shell
> create [chp|emg]
```

覇者の紋章または緊急クエストのテーブルを削除
```shell
> drop [chp|emg]
```

覇者の紋章または緊急クエストのテーブルの中身をすべて削除(テーブルは削除しない)
```shell
> clear [chp|emg]
```

覇者の紋章または緊急クエストの情報を取得し、データベースに書き込み
```shell
> get [chp|emg]
```

初期化コマンド(create -> get)を実行
```shell
> init
```

# ダウンロード
[ダウンロード](https://github.com/kousokujin/PSO2emergencyGetter/releases)

# Licence
Copyright (c) 2018 kousokujin.

Released under the MIT license.
