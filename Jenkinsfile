pipeline {
    agent any
    triggers {
        pollSCM '* * * * *'
    }
    stages {
        stage('Restore') {
            steps {
                sh '''
                cd vssln
                dotnet restore
                '''
            }
        }
        stage('Build') {
            steps {
                sh '''
                cd vssln
                dotnet build --configuration Release
                '''
            }
        }
        stage('Publish') {
            steps {
                sh '''
                cd vssln
                dotnet publish --configuration Release --output ../publish
                '''
            }
        }
    }
    post {
        always {
            script {
                def publishDir = 'publish'
                sh "ls -l ${publishDir}" // Debugging: Check if files exist
            }

            // Archive the correct publish directory
            archiveArtifacts artifacts: 'publish/**', allowEmptyArchive: true

            // Clean up workspace after archiving
            cleanWs()
        }
    }
}