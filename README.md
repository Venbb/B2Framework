# B2Framework

#### B2Framework UPM（Unity Package Manager）开发工程

#### 一、开发UPM

1、就在master分支开发并提交代码。

2、在“Assets/B2Framework”的Editor文件夹和Runtime文件夹添加asmdef文件，这样代码才会被引入项目引用到。

#### 二、发布UPM

1、执行git命令将Assets/B2Framework文件夹下的资源创建（合并）到upm分支：		

```shell
git subtree split --prefix=Assets/B2Framework --branch upm
或
git subtree split -P Assets/B2Framework -b upm
```

2、执行git命令创建Tag：		

```shell
git tag xxx upm（例：git tag 1.0.0 upm）
```

3、执行git命令推送到远端：

```shell
git push origin upm --tags
```

每次需要发布UPM时，都需要经过1、2、3步，每次发布时需要增加package.json文件里面的版本号，这里第2步创建Tag使用的版本号需要跟package.json文件保持一致。

#### 三、使用UPM

在需要引用的项目"Packages/manifest.json" 添加依赖：

```json
{
 "dependencies": {
     "com.venbb.b2framework": "https://github.com/Venbb/B2Framework.git#1.0.0",
     ...
 },
}
```

或者使用第三方库的UPM：https://github.com/mob-sakai/UpmGitExtension
在Package Manager添加git url：https://github.com/mob-sakai/UpmGitExtension.git，这样就可以可视化添加UPM了。

##### 四、参考

https://www.jianshu.com/p/153841d65846

https://blog.csdn.net/qq_29261149/article/details/108742884

https://www.cnblogs.com/leixinyue/p/11066438.html
