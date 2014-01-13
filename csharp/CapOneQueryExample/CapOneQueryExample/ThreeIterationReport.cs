using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CapOneQueryExample
{
	public static class ThreeIterationReport
	{
		public static string Query = @"
            with:
              $teams: whatever
              $sprintName: whatever
              $scheduleName: IT DEV 2-2-1, 2 Week Schedule
            from: Timebox
            find: $sprintName
            findin: 
              - Name
            where:
              Schedule.Name: $scheduleName
            select:
              - from: Workitems:Story
                select:
                  - Number
                  - Name
                  - Status.Name
                  - Team.Name
                  - Estimate
                  - Priority.Name
                  - Priority.Order
                  - from: Children:Task
                    select:
                      - Name
                      - Status.Name
                      - Owners.Name
                      - DetailEstimate
                      - Actuals.Value.@Sum
                      - ToDo
                  - from: Super:Epic
                    select:
                      - Number
                      - Name
                      - Subs:PrimaryWorkitem.Estimate.@Sum
                      - Swag
                      - Scope.Name
                      - AssetType
                      - Description
            ";

		public class StoryDto
		{
			public string Number { get; set; }

			[JsonProperty("Status.Name")]
			public string Status_Name { get; set; }

			[JsonProperty("Team.Name")]
			public string Team_Name { get; set; }

			public decimal Estimate { get; set; }

			[JsonProperty("Priority.Name")]
			public string Priority_Name { get; set; }

			[JsonProperty("Priority.Order")]
			public decimal Priority_Order { get; set; }

			[JsonProperty("Children:Task")]
			public List<TaskDto> Tasks { get; set; }

			[JsonProperty("Super:Epic")]
			public List<EpicDto> ParentEpic { get; set; }

			public class TaskDto
			{
				public string Type { get; set; }

				public string Name { get; set; }

				[JsonProperty("Status.Name")]
				public string Status_Name { get; set; }

				[JsonProperty("Owners.Name")]
				public List<string> Owners_Name { get; set; }

				public decimal DetailEstimate { get; set; }

				[JsonProperty("Actuals.Value.@Sum")]
				public decimal Actuals_Value_Sum { get; set; }

				public decimal ToDo { get; set; }
			}

			public class EpicDto
			{
				public string Number { get; set; }

				public string Name { get; set; }

				[JsonProperty("Subs:PrimaryWorkitem.Estimate.@Sum")]
				public string Subs_PrimaryWorkitem_Estimate_Sum { get; set; }

				public decimal Swag { get; set; }

				[JsonProperty("Scope.Name")]
				public string Scope_Name { get; set; }

				public string AssetType { get; set; }

				public string Description { get; set; }
			}

			public override string ToString()
			{
				var sb = new StringBuilder();
				sb.AppendLine(this.Number);
				sb.AppendLine(this.Team_Name);
				sb.AppendLine(this.Estimate.ToString());

				var epic = this.ParentEpic.First();
				sb.AppendLine(epic.Number);
				sb.AppendLine(epic.Swag.ToString());

				foreach (var taskDto in this.Tasks)
				{
					sb.AppendLine(taskDto.Name);
					sb.AppendLine(taskDto.Actuals_Value_Sum.ToString());
				}

				return sb.ToString();
			}
		}
	}
}