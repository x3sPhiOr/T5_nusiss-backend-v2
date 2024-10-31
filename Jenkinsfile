node {
  stage('SCM') {
    checkout scm
  }
  stage('SonarQube Analysis') {
    def scannerHome = tool 'SonarQubeScanner'
    withSonarQubeEnv() {
      sh "dotnet /var/jenkins_home/tools/hudson.plugins.sonar.SonarRunnerInstallation/SonarQubeScanner/SonarScanner.MSBuild.dll begin /k:\"T5_nusiss-backend-v3\""
      sh "dotnet build"
      sh "dotnet /var/jenkins_home/tools/hudson.plugins.sonar.SonarRunnerInstallation/SonarQubeScanner/SonarScanner.MSBuild.dll end"
    }
  }
}