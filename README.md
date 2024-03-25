# Blackbird.io Translate5

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

Cloud translation, review, post-editing and terminology platform. The mission of translate5 is the community-driven and continuous development of the open source and cloud-based translation management system of the language industry.

## Connecting 

1. Navigate to apps and search for Translate5. If you cannot find Translate5, then click _Add App_ in the top right corner, select Translate5, and add the app to your Blackbird environment.
2. Click _Add Connection_.
3. Name your connection for future reference, e.g., 'My client'.
4. In the _URL_ field, input your service URL (it should follow the next structure: https://[domain]), f.e.: https://translate5.net
5. In the _API Key_ field, input your API Key
6. Click _Connect_.
7. Confirm that the connection has appeared and the status is _Connected_.

## Actions

### Comment

- **List comments**: This action retrieves all comments for a specific segment within a translation task
- **Create Comment**: Create a new comment
- **Delete Comment**: Delete specific comment: This allows for the removal of a particular comment from a segment

### Segment

- **List segments**: Retrieves all segments associated with a specific task
- **Get Segment**: Get specific segment
- **Search Segments**: Search segments in task. Performs a search within the segments of a task based on specific criteria, such as text content
- **Translate Segment**: Translate a specific segment. Initiates the translation of a specified segment. 

### Task

- **List all tasks**: Retrieves a comprehensive list of all tasks within the system. This functionality is crucial for users needing to navigate through various tasks, providing a bird's-eye view of ongoing and completed translation projects.
- **Get Task**: Get specific task: Fetches detailed information about a particular task using its unique ID. This action allows users to dive deep into a single task's specifics, offering insights into its progress, associated languages, and other pertinent details.
- **Create Task**: Create new task: Initiates a new translation task with specified parameters, including the task's name, source and target languages, and associated customer information. This feature is essential for setting up translation projects tailored to specific needs and requirements.
- **Change Task Name**: Change task name: Updates the name of an existing task. This adjustment capability is important for maintaining relevance and clarity as projects evolve over time.
- **Delete Task**: Delete task: Removes a task from the system. This function is vital for cleaning up completed or obsolete tasks, ensuring that the workspace remains organized and focused.
- **Export Translated File**: Export translated file by task ID: Facilitates the downloading of translated content associated with a specific task. This action is key for retrieving final translations, making it easy for users to access and use their translated documents.
- **Create Task from ZIP**: Create task from ZIP: Allows for the creation of a task by uploading a ZIP file containing all necessary documents and resources for translation. This method simplifies the process of setting up a translation project by enabling bulk upload of materials, making it highly efficient for managing large or complex tasks.

### Translations

- **Translate Text**: Translate text instantly using specified source and target languages, alongside a chosen language resource.

- **Translate File**: Translate entire files by uploading a document and specifying languages. The Translate5 service performs the translation and returns a downloadable translated document.

- **Write Translation Memory**: Integrate translated segments into OpenTM2, enhancing the translation memory's richness and utility.

### User

- **List all users**: List all users

## Webhooks

- **On task import finished**: This webhook is triggered once the import process of a task is completed

- **On Task Finished**: Similar to the task import webhook, this notification is activated when a task reaches its completion

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
