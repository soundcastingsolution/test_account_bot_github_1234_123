import Dependencies._

lazy val root = (project in file(".")).
  settings(
    inThisBuild(List(
      organization := "com.example",
      scalaVersion := "2.12.5",
      version      := "0.1.0-SNAPSHOT"
    )),
    name := "Output",
     libraryDependencies ++= Seq(
	 "org.scalaj" %% "scalaj-http" % "2.4.1",
	"io.spray" %% "spray-json" % "1.3.4",
	"com.typesafe" % "config" % "1.3.2")
  )
