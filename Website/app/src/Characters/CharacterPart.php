<?php

namespace App\CharacterDatabase;

use SilverStripe\Assets\Image;
use SilverStripe\ORM\DataObject;
use App\CharacterDatabase\UserData;
use SilverStripe\Security\Permission;

/**
 * Class \App\Database\Experience
 *
 * @property string $Title
 * @property string $Type
 * @property int $RequiredXP
 * @property int $ImageID
 * @method \SilverStripe\Assets\Image Image()
 */
class CharacterPart extends DataObject
{
    private static $db = [
        "Title" => "Varchar(255)",
        "Type" => "Enum('None, SkinColor, Eyes, Mouth, Hair, Bottom, Top, Hat', 'None')",
        "RequiredXP" => "Int",
    ];

    private static $has_one = [
        "Image" => Image::class,
    ];

    private static $owns = [
        "Image",
    ];

    private static $belongs_many = [
        "UserData" => UserData::class,
    ];

    private static $summary_fields = [
        "ID" => "ID",
        "Title" => "Title",
        "Type" => "Type",
    ];

    private static $field_labels = [
    ];

    private static $default_sort = "Type ASC, ID ASC";

    private static $table_name = "CharacterPart";

    private static $singular_name = "Part";
    private static $plural_name = "Parts";

    private static $url_segment = "character-part";

    public function getCMSFields()
    {
        $fields = parent::getCMSFields();
        return $fields;
    }

    public function canView($member = null)
    {
        return true;
    }

    public function canEdit($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canDelete($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canCreate($member = null, $context = [])
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }
}
