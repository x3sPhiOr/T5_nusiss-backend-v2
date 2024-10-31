pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
            args '-v /var/run/docker.sock:/var/run/docker.sock'
        }
    }
    
    environment {
        SONAR_PROJECT_KEY = 'T5_nusiss-backend-v3'
        SONAR_HOST_URL = 'http://192.168.1.89:9000/'
    }

    tools {
        'SonarQube Scanner' 'SonarQubeScanner'  // Add this tools section
    }

    stages {
        stage('SCM') {
            steps {
                checkout scm
            }
        }

        stage('Restore Dependencies') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Clean') {
            steps {
                sh 'dotnet clean'
            }
        }

        stage('SonarQube Analysis') {
            environment {
                SONAR_SCANNER_OPTS = '-Xmx1g'
            }
            steps {
                script {
                    def scannerHome = tool 'SonarQubeScanner'
                    withSonarQubeEnv('SonarQube') {
                        sh """
                            dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin /k:"${SONAR_PROJECT_KEY}" 
                            dotnet build --no-restore
                            dotnet ${scannerHome}/SonarScanner.MSBuild.dll end
                        """
                    }
                }
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test --no-restore --verbosity normal --collect:"XPlat Code Coverage" --logger trx --results-directory ./TestResults'
            }
        }

        stage('Publish') {
            steps {
                sh 'dotnet publish --configuration Release --no-build --output ./publish'
            }
        }
    }

    post {
        always {
            cleanWs()
        }
        success {
            echo 'Build succeeded!'
        }
        failure {
            echo 'Build failed!'
        }
    }
}